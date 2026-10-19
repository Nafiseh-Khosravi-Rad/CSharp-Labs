# C Sharp system development labs

I developed these C# console and Windows Forms projects to practice program logic, object-oriented models, desktop interfaces and file-based data handling.

## Lab progression

- Lab-01: console exercises for output, structured programming, temperature conversion and payroll calculations.
- Lab-02: a Windows Forms payroll calculator.
- Lab-03: payroll models and pay-stub forms, including employee, department, position and qualification classes.
- Lab-04: payroll menus, record forms, employee payments and payroll history.

Each lab retains its separate solution and project structure. Source, designer and required resource files are included; IDE caches and generated binaries are excluded.

## Build and run

Install the .NET 10 SDK. Windows is required for Windows Forms projects. For example, from the repository root:

```sh
dotnet build Lab-01/HelloWorldProject/HelloWorldProject/HelloWorldProject.csproj -c Release
dotnet run --project Lab-01/HelloWorldProject/HelloWorldProject/HelloWorldProject.csproj
```

## Validation and limitations

All seven supplied C# projects passed Release builds on 19 October 2026. This establishes compilation, not interactive GUI or payroll-calculation correctness. Build warnings are not described as errors or silently treated as a warning-free result.

Payroll and employee datasets are excluded. Data-dependent forms require synthetic input files matching the source schema before use. No real payroll records, private reports or Office temporary files are included. These projects are coursework and are not payroll compliance software.
