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
|GET|api/goods/|Получает список всех работ| | `[workRequestApiModel]` | 200 OK |
|GET|api/goods/{id}|Получает работ с идентификатором id | fromRoute: id | `workApiModel` | 200 OK<br/>404 NotFound |
|POST|api/goods/|Добавляет новый работ| fromBody: `workRequestApiModel` | `workApiModel` | 200 OK |
|PUT|api/goods/{id}|Редактируем работ с идентификатором id| fromRoute: id <br/>fromBody: `workRequestApiModel` | `workApiModel` | 200 OK<br/>404 NotFound |
|DELETE|api/goods/{id}|Удаляет работ с идентификатором id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// workApiModel
{
  id: 1,
  name: "Работа 1",
  description: "описание работы 1"
}
```
```javascript

// workRequestApiModel
{
  name: "Работа 1",
  description: "описание работы 1"
}
```
