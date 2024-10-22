// замените ${Name} на название вашей миграции

cd BooksCatalogApi/Test.Work.BooksCatalogApi.Database
dotnet restore
dotnet ef -h
dotnet ef migrations add ${Name} --verbose --project ../../BooksCatalogApi/Test.Work.BooksCatalogApi.Database --startup-project ../../BooksCatalogApi/Test.Work.BooksCatalogApi.Migrator
dotnet ef database update --verbose --project ../../BooksCatalogApi/Test.Work.BooksCatalogApi.Database --startup-project ../../BooksCatalogApi/Test.Work.BooksCatalogApi.Migrator
