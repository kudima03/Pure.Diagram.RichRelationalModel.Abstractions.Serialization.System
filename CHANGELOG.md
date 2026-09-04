# Changelog

All notable changes to Pure.Diagram.RichRelationalModel.Abstractions.Serialization.System
are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.1.0.1] — 2026-06-07

- Maintenance release: dependency and build updates.

## [0.1.0-preview.1.0.0] — 2026-04-20

### Changed

- **Breaking:** `SeriesRichRelationalModelConverter` renamed to
  `DiagramSeriesRichRelationalModelConverter`, matching the
  `ISeriesRichRelationalModel` → `IDiagramSeriesRichRelationalModel` rename in
  `Pure.Diagram.RichRelationalModel.Abstractions` `0.1.0-preview.1.0.0`.
- Updated the `Pure.Diagram.RichRelationalModel.Abstractions`,
  `Pure.Diagram.RichRelationalModel`, and
  `Pure.Diagram.RichRelationalModel.HashCodes` dependencies to
  `0.1.0-preview.1.0.0`.

## [0.1.0-preview.0.1.0] — 2026-04-02

### Added

Initial release. `System.Text.Json` converters for the
`Pure.Diagram.RichRelationalModel.Abstractions` types:

- **`DiagramTypeRichRelationalModelConverter`** — converts
  `IDiagramTypeRichRelationalModel`.
- **`SeriesRichRelationalModelConverter`** — converts
  `ISeriesRichRelationalModel`.
- **`DiagramRichRelationalModelConverter`** — converts
  `IDiagramRichRelationalModel`, including its nested `Type` and `Series`.
- **`DiagramRichRelationalModelAbstractionsConverters`** — an
  `IEnumerable<JsonConverter>` bundling all three converters above for
  registration with `JsonSerializerOptions`.
