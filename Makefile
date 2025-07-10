up:
	@set +a
	@source .env || true
	@set -a
	@docker network create fortress-of-the-fallen || true
	@cd docker && docker compose -f docker-compose.yaml \
		-f docker-compose.database.yaml \
		-f docker-compose.ui.yaml up -d
	@cd docker && docker compose build
	@echo "-----------------------------------"
	@printf "\033[36mMongo Express: http://localhost:8010\033[36m\n"
	@printf "\033[36mRedisInsight: http://localhost:8011\033[0m\n"
	@printf "\033[36mSwagger: http://localhost:8080/docs/index.html\033[0m\n"
	@echo "-----------------------------------"

down:
	@cd docker && docker compose -f docker-compose.yaml \
		-f docker-compose.database.yaml \
		-f docker-compose.ui.yaml down
	@docker network rm fortress-of-the-fallen || true

dev:
	@cd docker && docker compose -f docker-compose.yaml \
		-f docker-compose.database.yaml \
		-f docker-compose.ui.yaml up -d
	@echo "-----------------------------------"
	@printf "\033[36mMongo Express: http://localhost:8010\033[36m\n"
	@printf "\033[36mRedisInsight: http://localhost:8011\033[0m\n"
	@echo "-----------------------------------"

build:
	@cd src/Presentation && dotnet build
	@cd src/Domain && dotnet build
	@cd src/Infrastructure && dotnet build
	@cd src/Application && dotnet build

clean:
	@rm -rf docker/data || true
	@cd src/Presentation && dotnet clean || true
	@cd src/Domain && dotnet clean || true
	@cd src/Infrastructure && dotnet clean || true
	@cd src/Application && dotnet clean || true

docker-ins:
	@set +a
	@source ./.env || true
	@set -a
	@docker network create fortress-of-the-fallen || true
	@cd docker && docker compose \
		-f docker-compose.database.yaml \
		-f docker-compose.ui.yaml up -d
	@echo "-----------------------------------"
	@printf "\033[36mMongo Express: http://localhost:8010\033[36m\n"
	@printf "\033[36mRedisInsight: http://localhost:8011\033[0m\n"
	@printf "\033[36mSwagger: http://localhost:8080/docs/index.html\033[0m\n"
	@echo "-----------------------------------"
	
