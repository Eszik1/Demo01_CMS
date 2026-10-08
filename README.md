# Demo01_CMS — переход от Demo_1 к Demo_2

Учебный проект по разделению ответственности между классами.
Основа: [Demo_1 от AKarkanitsa](https://github.com/AKarkanitsa/Demo01_CMS).
Пример структуры: [aleksejbernatovic/Demo01_CMS](https://github.com/aleksejbernatovic/Demo01_CMS).

## Что изменено

- `Customer`, `Product`, `Order` и `OrderItem` описывают данные и проверяют их через `Validate()`.
- `Retrieve()` и `Save()` перенесены в `CustomerRepository`, `ProductRepository`, `OrderRepository` и `OrderItemRepository`. Методы `Save` принимают соответствующий объект.
- `Address` содержит `StreetLine1`, `StreetLine2`, `City`, `State`, `PostalCode` и `Country`.
- У `Customer` появились `HomeAddress` и `WorkAddress`, у `Order` — `Customer` и `ShippingAddress`.
- Все классы бизнес-слоя используют пространство имён `CMS.BusinessLayer`.

`OrderItemRepository` — дополнение к финальному слайду Demo_2, сделанное по выбранному варианту примера.
Репозитории сохраняют учебные заглушки исходного проекта: `Retrieve(id)` создаёт объект с указанным ID,
`CustomerRepository.Retrieve()` возвращает пустой список, `Save(...)` возвращает `true`.
Подключение базы данных и реальное сохранение в это задание не входят.

## Сборка и тесты

Нужен **.NET 8 SDK**. Решение `CMS.sln` можно открыть в Rider или Visual Studio.
Файлы проектов переведены с .NET Framework 4.5.1 на .NET 8 и SDK-формат;
правила валидации из Demo_1 сохранены.

```sh
dotnet restore CMS.sln
dotnet build CMS.sln
dotnet test CMS.sln
```

`ACM.BL` — библиотека бизнес-классов, `ACM.BLTest` — тесты MSTest.
Это библиотека и набор тестов; отдельного интерфейса или исполняемой программы в исходном Demo_1 нет.

## Почему обязанности разделены

Бизнес-классы не зависят от способа хранения данных: при подключении базы меняется код репозиториев.
Каждый класс отвечает за одну задачу. Один тип `Address` используется для адресов клиента и доставки.
