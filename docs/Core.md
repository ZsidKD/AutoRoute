# AutoRoute

AutoRoute is a .NET/C# library for building automation workflows when an application or service does not expose a convenient API.

The project is split conceptually into two parts:

- `AutoRoute.Abstarctions` - public contracts and models.
- `AutoRoute` - core engine that executes routes and coordinates handlers.

This document is the implementation map for the core project. It does not change the architecture. It explains what each class should do, what it calls, and what it depends on.

## Core Goal

The core project should do only one thing:

- take a route,
- resolve the right handler for each command,
- execute the command in the current context,
- collect results,
- report errors in a predictable way.

The core must not contain platform-specific logic such as Android, Windows, Playwright, OCR, or ADB details.

## Design Rules For `AutoRoute`

- The core must be platform-agnostic.
- The core must know about commands, routes, results, context, registry, dispatcher, and pipeline.
- The core must not know how Android or Windows performs the action internally.
- The core should be easy to test in memory.
- Each class should have one responsibility.
- Data models should stay simple and stable.

## Development Order

Build the core in this order:

1. Exceptions.
2. Execution options.
3. Result models.
4. Execution context.
5. Route model and validation.
6. Command handler abstraction.
7. Handler registry.
8. Command dispatcher.
9. Route executor.
10. Pipeline and middleware.
11. Converters and adapters for input/output types.
12. Tests for the whole execution flow.

## Folder Map

If the project later gets split into `AutoRoute.Core`, these files can move there without changing the design.

```text
AutoRoute/
  Exceptions/
  Options/
  Results/
  Context/
  Routing/
  Execution/
  Pipeline/
  Converters/
  Tests/
```

## Exceptions

These classes should be created very early because every execution flow needs a clean error model.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `AutoRouteException` | Base exception for all core and platform errors. | Nothing. It is only thrown and caught. | `System.Exception` |
| `RouteValidationException` | Signals that a route is structurally invalid before execution starts. | `RouteValidator` throws it. | `AutoRouteException` |
| `ExecutionTimeoutException` | Signals that execution exceeded the allowed time. | `AutomationExecutor`, pipeline timeout logic. | `AutoRouteException` |
| `UnsupportedCommandException` | Signals that the current runtime does not support a command. | `CommandDispatcher` or handler lookup. | `AutoRouteException` |
| `CommandExecutionException` | Wraps a failure that happened during a command run. | `CommandDispatcher`, command handlers. | `AutoRouteException` |

## Options

These classes define the runtime policy of the engine.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `ExecutionOptions` | Holds timeout, retry, logging, and continuation settings. | Read by `AutomationExecutor` and pipeline. | `TimeSpan`, primitive settings |
| `RetryPolicy` | Describes how many times and under which conditions a command can be retried. | Used by pipeline or executor retry logic. | Primitive settings |
| `RouteExecutionMode` | Defines whether execution stops on first error or continues. | Used by `AutomationExecutor`. | Enum values |

## Results

Results should be immutable or at least stable after creation.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `CommandResult` | Describes the outcome of one command. | Created by handlers and normalized by dispatcher. | `ICommandResult`, error info, output model |
| `ExecutionResult` | Describes the outcome of the whole route. | Created by `AutomationExecutor`. | `CommandResult`, route metadata |
| `ExecutionStatus` | Represents the final state of execution. | Used by `CommandResult` and `ExecutionResult`. | Enum values |

What `CommandResult` should mean:

- did the command succeed;
- what message or error happened;
- what value came back;
- how long it took.

What `ExecutionResult` should mean:

- did the route finish;
- which command failed;
- how many commands were executed;
- what the final outcome was;
- how long the whole route took.

## Context

The context is the shared state passed through the whole execution.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `ExecutionContext` | Holds shared data, services, options, and execution state. | Read and updated by executor, dispatcher, handlers, and pipeline. | `IExecutionContext`, service access, shared state storage |
| `CommandContext` | Holds data specific to a single command execution. | Created by executor and passed into command handling. | `ExecutionContext`, command metadata |
| `AutomationContext` | Holds top-level shared state for the whole automation session. | Used by executor and any shared services. | Shared data storage |

What context should be used for:

- storing intermediate values;
- passing data from one step to another;
- sharing services like logging or device access;
- carrying execution options;
- keeping track of the current route step.

## Routing

Routing describes the sequence of actions, not how they are executed.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `Route` | Stores the ordered list of commands for one automation flow. | Read by `AutomationExecutor`. | `IRoute`, command collection |
| `RouteBuilder` | Builds a route in a fluent way. | Adds commands and finally creates a `Route`. | `IAutomationCommand`, `IRoute` |
| `RouteValidator` | Checks that a route is valid before execution begins. | Called by `AutomationExecutor` before running the route. | Route model, supported command rules |
| `RouteStep` | Represents one step inside the route when you want to keep step metadata separate from command data. | Used by `Route` and possibly by logging. | Command metadata, step metadata |

Architectural note:

- `Route` should stay dumb and only hold data.
- `RouteBuilder` is convenience API, not business logic.
- `RouteValidator` should fail early and explain why.

## Execution

This is the actual engine of the core.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `AutomationExecutor` | Orchestrates the whole route execution from start to finish. | Calls `RouteValidator`, `CommandDispatcher`, pipeline, and result builders. | `IRoute`, `ExecutionContext`, `ExecutionOptions`, results |
| `ICommandHandler<TCommand>` | Describes a handler that can execute one command type. | Implemented by platform-specific or core handlers. | Command type, `CommandResult` |
| `CommandDispatcher` | Resolves the right handler for a command and delegates execution to it. | Calls a handler selected from the registry. | `CommandHandlerRegistry`, `ICommandHandler<TCommand>` |
| `CommandHandlerRegistry` | Stores and resolves command handlers by command type. | Used by dispatcher and executor startup. | Handler registrations, type lookup |

What `AutomationExecutor` should do in order:

1. Validate route.
2. Create or receive execution context.
3. Prepare execution pipeline.
4. For each command, ask dispatcher to execute it.
5. Collect each `CommandResult`.
6. Stop or continue depending on `ExecutionOptions`.
7. Produce `ExecutionResult`.

What `CommandDispatcher` should do:

- find a handler for the command type;
- pass the command and context to the handler;
- normalize errors into `CommandResult` or exceptions;
- avoid knowing anything about Android or Windows internals.

## Pipeline

Pipeline is optional for the first working version, but it is a strong extension point.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `ExecutionPipeline` | Runs middleware around command execution. | Wraps dispatcher or handler calls. | Middleware list, execution context |
| `IExecutionMiddleware` | Describes one middleware step around execution. | Implemented by logging, retry, timeout, or diagnostics features. | `ExecutionContext`, `CommandResult` |

Good pipeline use cases:

- logging;
- timing;
- retry;
- timeout enforcement;
- diagnostics;
- recovery hooks.

## Converters

This area becomes useful when input and output types need normalization.

| Class | What it does | Calls | Depends on |
| --- | --- | --- | --- |
| `IInputConverter` | Converts a declared input type into a shape a handler can use. | Called by dispatcher, handler, or executor when needed. | `IInputType`, target command data |
| `IOutputConverter` | Converts handler output into a declared output type. | Called after command execution if output normalization is needed. | `IOutputType`, result data |
| `ConverterRegistry` | Resolves the right converter for a type. | Used by executor or dispatcher. | Registered converters |

This is where your idea about input/output types becomes practical:

- the type describes what the data is;
- the converter describes how to adapt it;
- the executor decides whether conversion is required.

## How The Flow Should Look

The execution chain should stay simple:

`Route` -> `AutomationExecutor` -> `CommandDispatcher` -> `ICommandHandler<TCommand>` -> `CommandResult` -> `ExecutionResult`

Context is threaded through the whole flow:

`ExecutionContext` -> route validation -> command execution -> result collection

## What The Core Must Not Do

- no Android-specific ADB code;
- no Windows-specific UI Automation code;
- no Playwright page automation code;
- no direct parser implementation for a specific site;
- no service registration extensions that belong to the facade layer;
- no heavy UI element model unless it is truly required by the core contract.

## Suggested First Implementation Slice

If you want the smallest useful starting point, implement these first:

1. `AutoRouteException`.
2. `ExecutionOptions`.
3. `CommandResult`.
4. `ExecutionResult`.
5. `ExecutionContext`.
6. `Route`.
7. `RouteValidator`.
8. `ICommandHandler<TCommand>`.
9. `CommandHandlerRegistry`.
10. `CommandDispatcher`.
11. `AutomationExecutor`.

That gives you a working engine skeleton.

## Practical Rule For Writing Each Class

Before coding any class, answer these three questions:

1. What is its single responsibility?
2. What does it need to know about the rest of the system?
3. What should it return, store, or delegate?

If the answer is unclear, the class is probably too early or too broad.

