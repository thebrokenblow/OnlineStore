# Online Store

## Тестовое задание на младшего backend разработчика

Простой веб-сервис, который предоставляет RESTful API для управления
товарами и категориями товаров.

1. С клонируйте проект на свой компьютер

```sh
    git clone https://github.com/thebrokenblow/OnlineStore.git
```

2. Для запуска необходимо скачать .NET 8 SDK под вашу операционную систему
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0

3. Далее запустить solution проекта

4. Для миграции объектов в базу данных необходимо установить вашу строку подключения DbConnection в файле appsettings.json проекта OnlineShop.WebApi

![appsettings.json](https://github.com/thebrokenblow/OnlineStore/blob/master/photos/appsettings.png)

5. Далее необходимо зайти в раздел Средства -> Диспетчер пакетов NuGet -> Консоль диспетчера пакетов

![Консоль диспетчера пакетов](https://github.com/thebrokenblow/OnlineStore/blob/master/photos/Console1.png)

6. В качестве запускаемого проекта надо выбрать OnlineShop.WebApi а проект по умолчанию OnlineShop.Persistence

![OnlineShop.Persistence](https://github.com/thebrokenblow/OnlineStore/blob/master/photos/Console2.png)

7. Ввести две команды 
```sh
   Add-Migration <Название миграции>
   Update-Database
```

   ![Миграция](https://github.com/thebrokenblow/OnlineStore/blob/master/photos/Migration.png)

8. Если миграция прошла успешно, то можно запускать проект нажав на F5 или F5 + Fn

В приложении подключён Swagger для удобства тестирования.
