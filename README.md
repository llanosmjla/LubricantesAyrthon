# Sistema de Ventas de Lubricantes Ayrthon – API

Este proyecto es una **API de sistema de ventas** desarrollada en **.NET 9**, implementada siguiendo el enfoque **Test Driven Development (TDD)**. Incluye **pruebas unitarias** para asegurar la calidad y confiabilidad del software, utilizando técnicas de aislamiento como **mocks, stubs y fakes**.  

El proyecto cuenta con endpoints para la gestión de **clientes, productos, vendedores y facturas**, incluyendo validaciones de stock y escenarios de errores.

---

## Requisitos

- [.NET 9](https://dotnet.microsoft.com/download)  
- Visual Studio 2022 o Visual Studio Code  
- Git  

---

## Instalación

1. Clonar el repositorio:  
```bash
git clone https://github.com/tu-usuario/tu-repositorio.git
cd tu-repositorio
```
2. Restaurar paquetes NuGet
```bash
dotnet restore
```
4. Instalar paquetes necesarios para cobertura de pruebas:
```bash
dotnet add package coverlet.collector
dotnet add package reportgenerator
```

## Ejecución del proyecto

Para ejecutar la API en modo desarrollo:
```bash
dotnet run
```

## Ejecución de pruebas unitarias

Para ejecutar las pruebas unitarias:
```bash
dotnet test
```

## Generación de cobertura de pruebas

1. Ejecutar pruebas con Coverlet:
```bash
dotnet test --collect:"XPlat Code Coverage"
```
2. Generar reporte en HTML con ReportGenerator:
```bash
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
```
3. Abrir el informe en tu navegador desde la carpeta coverage-report/index.html.

## Estructura del proyecto
```rust
/src
    /NombreDelProyecto.Api      -> Proyecto principal de la API
    /NombreDelProyecto.Tests    -> Proyecto de pruebas unitarias
        /coverage-report        -> Reporte de cobertura generado
README.md                       -> Este archivo
```
