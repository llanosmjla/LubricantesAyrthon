# ------------------------------
# Script completo para generar cobertura y reporte HTML
# ------------------------------

# 1️⃣ Determinar la raíz del proyecto de test
$root = Resolve-Path ".."        # carpeta padre del script
$coverageDir = Join-Path $root "coverage-report"
$testResultsDir = Join-Path $root "TestResults"

# 2️⃣ Limpiar resultados antiguos
Write-Host "Cleaning old test results and coverage report..."
Remove-Item -Recurse -Force $testResultsDir -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force $coverageDir -ErrorAction SilentlyContinue

# 3️⃣ Detectar automáticamente el proyecto de test (*.csproj)
$testProject = Get-ChildItem -Path $root -Recurse -Filter *.csproj |
               Where-Object { $_.Name -like "*Test*" } |
               Select-Object -First 1

if (-not $testProject) {
    Write-Error "No test project (*.csproj) found in the solution."
    exit 1
}

Write-Host "Found test project: $($testProject.FullName)"

# 4️⃣ Ejecutar los tests con cobertura
Write-Host "Running tests with XPlat Code Coverage..."
dotnet test $testProject.FullName --collect:"XPlat Code Coverage"

# 5️⃣ Detectar automáticamente el archivo cobertura generado
$coverageFile = Get-ChildItem -Path $testResultsDir -Recurse -Filter coverage.cobertura.xml | Select-Object -First 1

if (-not $coverageFile) {
    Write-Error "No coverage file found."
    exit 1
}

Write-Host "Found coverage file: $($coverageFile.FullName)"

# 6️⃣ Generar reporte HTML con ReportGenerator
Write-Host "Generating coverage report..."
reportgenerator -reports:$coverageFile.FullName -targetdir:$coverageDir -reporttypes:Html

# 7️⃣ Abrir el reporte automáticamente
$indexFile = Join-Path $coverageDir "index.htm"
if (Test-Path $indexFile) {
    Start-Process $indexFile
    Write-Host "Coverage report generated and opened at $coverageDir"
} else {
    Write-Warning "Report generated but index.htm not found."
}

