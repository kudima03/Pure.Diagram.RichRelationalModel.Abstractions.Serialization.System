# Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System

`System.Text.Json` converters for the **Pure.Diagram** rich relational model abstractions.

[![.NET build & test](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System)](https://www.nuget.org/packages/Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System` provides `System.Text.Json` converters that enable serialization and deserialization of the `IDiagram*RichRelationalModel` interfaces defined in [`Pure.Diagram.RichRelationalModel.Abstractions`](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions). Each converter maps between JSON and the corresponding interface by using an internal record type that implements the interface.

## Converters

| Type | Converts |
|---|---|
| `DiagramRichRelationalModelConverter` | `IDiagramRichRelationalModel` |
| `DiagramTypeRichRelationalModelConverter` | `IDiagramTypeRichRelationalModel` |
| `DiagramSeriesRichRelationalModelConverter` | `IDiagramSeriesRichRelationalModel` |
| `DiagramRichRelationalModelAbstractionsConverters` | `IEnumerable<JsonConverter>` containing all three converters above |

## Dependencies

- [`Pure.Diagram.RichRelationalModel.Abstractions`](https://github.com/kudima03/Pure.Diagram.RichRelationalModel.Abstractions) — composite `IDiagram*RichRelationalModel` interfaces
