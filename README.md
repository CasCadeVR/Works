# Акт сдачи-приёмки выполненных работ

![Акт сдачи-приёмки выполненных работ](target.png "Накладная")

## Схема базы данных
```mermaid
classDiagram
   class Customer {
    Guid Id
    String FullName
    String Occupation
    String Firm
    String TaxPayerId
   }

   class Executor {
    Guid Id
    String FullName
    String Occupation
    String Firm
    String RegistrationNumber
   }

   class Work {
    Guid Id
    String Name
    String Description
    Decimal Price
    String UnitOfMeasure
   }

   class ActWork {
    Guid Id
    Int Quantity
    Decimal ActualPrice
    Decimal TotalPrice
    Work Work
    Guid WorkId
    Act Act
    Guid ActId
   }

   class Act {
    Guid Id
    String ActNumber
    DateOnly Date
    Guid ExecutorId
    Executor Executor
    Guid CustomerId
    Customer Customer
    ICollection<ActWork> Works ActWorks
    Decimal AddedTax
   }

    Customer <|-- Act
    Executor <|-- Act
    Work <|--|> ActWork
    ActWork <|--|> Act
```

## Реализация API
### CRUD работ
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|Api/Works/|Получает список всех работ| | `[WorkApiModel]` | 200 OK |
|GET|Api/Works/{id}|Получает работу по идентификатору id | fromRoute: id | `WorkApiModel` | 200 OK<br/>404 NotFound |
|POST|Api/Works/|Добавляет новую работу| fromBody: `WorkRequestApiModel` | `WorkApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|Api/Works/{id}|Редактирует работу по идентификатору id| fromRoute: id <br/>fromBody: `WorkRequestApiModel` | `WorkApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|Api/Works/{id}|Удаляет работу по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


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
|GET|Api/Executor/|Получает список всех исполнителей| | `[ExecutorApiModel]` | 200 OK |
|GET|Api/Executor/{id}|Получает исполнителя по идентификатору id | fromRoute: id | `ExecutorApiModel` | 200 OK<br/>404 NotFound |
|POST|Api/Executor/|Добавляет нового исполнителя| fromBody: `ExecutorRequestApiModel` | `ExecutorApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|Api/Executor/{id}|Редактирует исполнителя по идентификатору id| fromRoute: id <br/>fromBody: `ExecutorRequestApiModel` | `ExecutorApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|Api/Executor/{id}|Удаляет исполнителя по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// ExecutorApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  FullName: "ФИО исполнителя",
  Occupation: "Работа исполнителя",
  Firm: "ООО Фирма исполнителя",
  RegistrationNumber: "1234567890123"
}
```
```javascript

// ExecutorRequestApiModel
{
  FullName: "ФИО исполнителя",
  Occupation: "Работа исполнителя",
  Firm: "ООО Фирма исполнителя",
  RegistrationNumber: "1234567890123"
}
```


### CRUD заказчиков
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|Api/Customer/|Получает список всех заказчиков| | `[CustomerApiModel]` | 200 OK |
|GET|Api/Customer/{id}|Получает заказчика по идентификатору id | fromRoute: id | `CustomerApiModel` | 200 OK<br/>404 NotFound |
|POST|Api/Customer/|Добавляет нового заказчика| fromBody: `CustomerRequestApiModel` | `CustomerApiModel` | 200 OK<br/>422 UnprocessableEntity |
|PUT|Api/Customer/{id}|Редактирует заказчика по идентификатору id| fromRoute: id <br/>fromBody: `CustomerRequestApiModel` | `CustomerApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity  |
|DELETE|Api/Customer/{id}|Удаляет заказчика по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// CustomerApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  FullName: "ФИО заказчика",
  Occupation: "Работа заказчика",
  Firm: "ООО Фирма заказчика",
  TaxPayerId: "123456789012"
}
```
```javascript

// CustomerRequestApiModel
{
  FullName: "ФИО заказчика",
  Occupation: "Работа заказчика",
  Firm: "ООО Фирма заказчика",
  TaxPayerId: "123456789012"
}
```


### CRUD актов
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|Api/Act/|Получает список всех актов| | `[ActApiModel]` | 200 OK |
|GET|Api/Act/{id}|Получает акт по идентификатору id | fromRoute: id | `ActApiModel` | 200 OK<br/>404 NotFound |
|GET|Api/Act/{id}/export| Экспортирует акт по идентификатору id | fromRoute: id | File .xlsx | 200 OK<br/>404 NotFound |
|POST|Api/Act/|Добавляет новый акт| fromBody: `ActRequestApiModel` | `ActApiModel` | 200 OK<br/>422 UnprocessableEntity<br/>409 Conflict |
|PUT|Api/Act/{id}|Редактирует акт по идентификатору id| fromRoute: id <br/>fromBody: `ActRequestApiModel` | `ActApiModel` | 200 OK<br/>404 NotFound<br/>422 UnprocessableEntity<br/>409 Conflict  |
|DELETE|Api/Act/{id}|Удаляет акт по идентификатору id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// ActApiModel
{
  Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
  ActNumber: "120019",
  Date: "2025-07-18T16:25:39.317",
  Executor: {
    Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
    FullName: "ФИО исполнителя",
    Occupation: "Работа исполнителя",
    Firm: "ООО Фирма исполнителя",
    RegistrationNumber: "1234567890123",
  },
  Customer: {
    Id: c2331ea8-a98d-4c3e-baea-d88f5665947,
    FullName: "ФИО заказчика",
    Occupation: "Работа заказчика",
    Firm: "ООО Фирма заказчика",
    TaxPayerId: "123456789012",
  },
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
  AddedTax: 14.4,
  TotalPriceAddedTax: 171598.86,
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
  AddedTax: 14.4
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