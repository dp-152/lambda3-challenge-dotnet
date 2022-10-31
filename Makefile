.PHONY: clean

mkfile_path := $(abspath $(lastword $(MAKEFILE_LIST)))
mkfile_dir := $(dir $(mkfile_path))

ifeq ($(OS),Windows_NT)
DOTNET  = dotnet.exe
CD      = cd.exe
NPM     = npm.exe
NPX     = npx.exe
COMPOSE = docker-compose.exe
else
DOTNET  = dotnet
CD      = cd
NPM     = npm
NPX		= npx
COMPOSE = docker-compose
endif

SLN     = $(mkfile_dir)CopaGames.sln
DEFPROJ = $(mkfile_dir)CopaGames.API
OUTDIR  = $(mkfile_dir)out
FRONTDIR = $(mkfile_dir)copa-front

GOTO_FRONTDIR = $(CD) $(FRONTDIR) &&

build-back:
	$(DOTNET) build $(SLN)

build-front:
	$(GOTO_FRONTDIR) $(NPM) ci && $(NPM) run build

buildall: build-front build-back

publish:
	$(DOTNET) publish $(SLN) -c Release -o $(OUTDIR)
	$(GOTO_FRONTDIR) $(NPM) ci && $(NPM) run build

restore:
	$(DOTNET) restore $(SLN)

npmci:
	$(GOTO_FRONTDIR) $(NPM) ci

install-deps: restore npmci

compose:
	$(COMPOSE) up --build -d

compose-cleanbuild:
	$(COMPOSE) build --no-cache && $(COMPOSE) up -d

clean:
	$(DOTNET) clean $(SLN)
	$(GOTO_FRONTDIR) $(NPX) rimraf build
