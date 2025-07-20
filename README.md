# Акт сдачи-приёмки выполненных работ

![Акт сдачи-приёмки выполненных работ](target.png "Накладная")

## Схема базы данных
```mermaid
---
title: Order example
---
erDiagram
    CUSTOMER ||--|| ACT: uses
    EXECUTOR ||--|| ACT: uses
    WORKS ||--o{ ACT_WORKS: uses
    ACT_WORKS }o--|| ACT: uses
```

## Реализация API
### CRUD работ
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/Works/|Получает список всех работ| | `[WorkApiModel]` | 200 OK |
|GET|api/Works/{id}|Получает работу по идентификатору id | fromRoute: id | `WorkApiModel` | 200 OK<br/>404 NotFound |
|POST|api/Works/|Добавляет новую работу| fromBody: `WorkRequestApiModel` | `WorkApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|api/Works/{id}|Редактирует работу по идентификатору id| fromRoute: id <br/>fromBody: `WorkRequestApiModel` | `WorkApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|api/Works/{id}|Удаляет работу по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// WorkApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  Name: "Работа 1",
  Description: "описание работы 1",
  Price: 10000,
  UnitOfMeasure: "кг."
}
```
```javascript

// WorkRequestApiModel
{
  Name: "Работа 1",
  Description: "описание работы 1",
  Price: 10000,
  UnitOfMeasure: "кг."
}
```


### CRUD исполнителей
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/Executor/|Получает список всех исполнителей| | `[ExecutorApiModel]` | 200 OK |
|GET|api/Executor/{id}|Получает исполнителя по идентификатору id | fromRoute: id | `ExecutorApiModel` | 200 OK<br/>404 NotFound |
|POST|api/Executor/|Добавляет нового исполнителя| fromBody: `ExecutorRequestApiModel` | `ExecutorApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|api/Executor/{id}|Редактирует исполнителя по идентификатору id| fromRoute: id <br/>fromBody: `ExecutorRequestApiModel` | `ExecutorApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|api/Executor/{id}|Удаляет исполнителя по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// ExecutorApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  FIO: "ФИО исполнителя",
  Occupation: "Работа исполнителя",
  Firm: "ООО Фирма исполнителя",
  OGRN: "1234567890123"
}
```
```javascript

// ExecutorRequestApiModel
{
  FIO: "ФИО исполнителя",
  Occupation: "Работа исполнителя",
  Firm: "ООО Фирма исполнителя",
  OGRN: "1234567890123"
}
```


### CRUD заказчиков
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/Customer/|Получает список всех заказчиков| | `[CustomerApiModel]` | 200 OK |
|GET|api/Customer/{id}|Получает заказчика по идентификатору id | fromRoute: id | `CustomerApiModel` | 200 OK<br/>404 NotFound |
|POST|api/Customer/|Добавляет нового заказчика| fromBody: `CustomerRequestApiModel` | `CustomerApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|api/Customer/{id}|Редактирует заказчика по идентификатору id| fromRoute: id <br/>fromBody: `CustomerRequestApiModel` | `CustomerApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|api/Customer/{id}|Удаляет заказчика по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// CustomerApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  FIO: "ФИО заказчика",
  Occupation: "Работа заказчика",
  Firm: "ООО Фирма заказчика",
  INN: "123456789012"
}
```
```javascript

// CustomerRequestApiModel
{
  FIO: "ФИО заказчика",
  Occupation: "Работа заказчика",
  Firm: "ООО Фирма заказчика",
  INN: "123456789012"
}
```


### CRUD актов
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/Act/|Получает список всех актов| | `[ActApiModel]` | 200 OK |
|GET|api/Act/{id}|Получает акт по идентификатору id | fromRoute: id | File .xlsx | 200 OK<br/>404 NotFound |
|GET|api/Act/{id}/export| Экспортирует акт по идентификатору id | fromRoute: id | `ActApiModel` | 200 OK<br/>404 NotFound |
|POST|api/Act/|Добавляет новый акт| fromBody: `ActRequestApiModel` | `ActApiModel` | 200 OK<br/>422 UnprocessableEntity<br/>409 Conflict |
|PUT|api/Act/{id}|Редактирует акт по идентификатору id| fromRoute: id <br/>fromBody: `ActRequestApiModel` | `ActApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity<br/>409 Conflict  |
|DELETE|api/Act/{id}|Удаляет акт по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// ActApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  ActNumber: "120019",
  Date: "2025-07-18T16:25:39.317",
  ExecutorId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  ExecutorFIO: "ФИО исполнителя",
  ExecutorOccupation: "Работа исполнителя",
  ExecutorFirm: "ООО Фирма исполнителя",
  ExecutorOGRN: "1234567890123",
  CustomerId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  CustomerFIO: "ФИО заказчика",
  CustomerOccupation: "Работа заказчика",
  CustomerFirm: "ООО Фирма заказчика",
  CustomerINN: "123456789012",
  Works: [
    {
      Quantity: 10,
      ActualPrice: 14999.9,
      TotalPrice: 149999,
      WorkId: c2331ea8-a98d-4c3e-baea-d88f5665947,
      WorkName: "Работа 1",
      WorkDescription: "Описание работы 1",
      WorkPrice: 13999.9,
      WorkUnitOfMeasure: "кг."
    }
  ],
  TotalPrice: 149999,
  NDS: 14.4,
  TotalPriceNDS: 171598.86,
}
```
```javascript

// ActRequestApiModel
{
  ActNumber: "120019",
  Date: "2025-07-18T16:25:39.317",
  ExecutorId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  CustomerId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  Works: [
    {
      WorkId: c2331ea8-a98d-4c3e-baea-d88f5665947,
      Quantity: 10,
      ActualPrice: 14999.9
    }
  ],
  NDS: 14.4
}
```
```javascript
// ActWorksApiModel
{
  Quantity: 10,
  ActualPrice: 14999.9,
  TotalPrice: 149999,
  WorkId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  WorkName: "Работа 1",
  WorkDescription: "Описание работы 1",
  WorkPrice: 13999.9,
  WorkUnitOfMeasure: "кг."
}
```
```javascript

// ActWorksRequestApiModel
{
  WorkId: c2331ea8-a98d-4c3e-baea-d88f5665947,
  Quantity: 10,
  ActualPrice: 14999.9
}
```
