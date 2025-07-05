# Акт сдачи-приёмки выполненных работ

![Акт сдачи-приёмки выполненных работ](target.png "Накладная")

## Схема базы данных
```mermaid
---
title: Order example
---
erDiagram
    CUSTOMER ||--o{ ACT: any
    EXECUTOR ||--o{ ACT: any
    WORKS }|..|{ ACT_WORKS: any
    ACT_WORKS }|..|{ ACT: any
```

## Реализация API
### CRUD работ
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/works/|Получает список всех работ| | `[workRequestApiModel]` | 200 OK |
|GET|api/works/{id}|Получает работ с идентификатором id | fromRoute: id | `workApiModel` | 200 OK<br/>404 NotFound |
|POST|api/works/|Добавляет новый работ| fromBody: `workRequestApiModel` | `workApiModel` | 200 OK |
|PUT|api/works/{id}|Редактируем работ с идентификатором id| fromRoute: id <br/>fromBody: `workRequestApiModel` | `workApiModel` | 200 OK<br/>404 NotFound |
|DELETE|api/works/{id}|Удаляет работ с идентификатором id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// workApiModel
{
  id: 1,
  name: "Работа 1",
  description: "описание работы 1",
  price: 10000
}
```
```javascript

// workRequestApiModel
{
  name: "Работа 1",
  description: "описание работы 1",
  price: 10000
}
```
