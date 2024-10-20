# Online Store

## Тестовое задание на младшего backend разработчика

Простой веб-сервис, который предоставляет RESTful API для управления
товарами и категориями товаров.

1. Склонируйте проект на свой компьютер

```sh
git clone https://github.com/thebrokenblow/OnlineStore.git
```

2. Для запуска необходимо скачать .NET 8 SDK под вашу операционную систему
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0

3. Далее запустить solution проекта

4. Для миграции данных в базу данных необходимо установить строку подкючения DbConnection в файле appsettings.json проекта OnlineShop.WebApi на вашу
   ![appsettings.json](http://url/to/img.png)

5. Далее необходимо зайти в раздел Средства -> Диспечер пакетов NuGet -> Консоль диспетчера пакетов
   ![Консоль диспетчера пакетов](http://url/to/img.png)

6. В качестве запускаемого проекта надо выбрать OnlineShop.WebApi а проект по умолчанию OnlineShop.Persistence
   ![OnlineShop.Persistence](http://url/to/img.png)

7. Ввести две команды Add-Migration <Название миграции> и Update-Database
   ![Миграция](http://url/to/img.png)

8. Если миграция прошла успешно, то можно запускать проект нажав на F5 или F5 + Fn
