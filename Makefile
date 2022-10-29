mkfile_path := $(abspath $(lastword $(MAKEFILE_LIST)))
mkfile_dir := $(dir $(mkfile_path))

ifeq ($(OS),Windows_NT)
DOTNET  = dotnet.exe
else
DOTNET  = dotnet
endif

SLN     = $(mkfile_dir)CopaGames.sln
DEFPROJ = $(mkfile_dir)CopaGames.API
OUTDIR  = $(mkfile_dir)out

build:
	$(DOTNET) build $(SLN)
clean:
	$(DOTNET) clean $(SLN)
restore:
	$(DOTNET) restore $(SLN)
watch:
	$(DOTNET) watch --project $(DEFPROJ) run
start:
	$(DOTNET) run --project $(DEFPROJ)
publish:
	$(DOTNET) publish $(SLN) -c Release -o $(OUTDIR)
