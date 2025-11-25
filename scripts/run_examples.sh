set -euo pipefail


echo "1) Compiling and running the transpilador (uses Mono fallback)..."
make run-mono

echo "\n2) Compiling and running the generated C# (ProgramaTranspilado)..."
make run-generated

echo "\nDone."
