up:
	@docker network create fortress-of-the-fallen || true
	@echo "-----------------------------------"
	@cd docker && docker compose -f docker-compose.database.yaml up -d
	@cd docker && docker compose up --build -d
	@echo "-----------------------------------"
	@printf "\033[36mMongo Express: http://localhost:8010\033[36m\n"
	@printf "\033[36mRedisInsight: http://localhost:8011\033[0m\n"
	@printf "\033[36mSwagger: http://localhost:8080/docs/index.html\033[0m\n"
	@echo "-----------------------------------"

db:
	@cd docker && docker compose -f docker-compose.database.yaml up -d
	@echo "-----------------------------------"
	@printf "\033[36mMongo Express: http://localhost:8010\033[36m\n"
	@printf "\033[36mRedisInsight: http://localhost:8011\033[0m\n"
	@echo "-----------------------------------"

dotnet:
	dotenv -e .env -- dotnet watch run --project ./src/Presentation/Presentation.csproj
