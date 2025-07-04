# Товарная накладная

![Накладная](target.png "Накладная")

## Схема базы данных
```mermaid
---
title: Order example
---
erDiagram
    SELLER ||--o{ ORDER: any
    BUYER ||--o{ ORDER: any
    GOODS }|..|{ ORDER_GOODS: any
    ORDER_GOODS }|..|{ ORDER: any
    EMPLOEES }|..|{ ORDER: any
```

## Реализация API
### CRUD товаров
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/works/|Получает список всех товаров| | `[goodApiModel]` | 200 OK |
|GET|api/works/{id}|Получает товар с идентификатором id | fromRoute: id | `goodApiModel` | 200 OK<br/>404 NotFound |
|POST|api/works/|Добавляет новый товар| fromBody: `goodRequestApiModel` | `goodApiModel` | 200 OK |
|PUT|api/works/{id}|Редактируем товар с идентификатором id| fromRoute: id <br/>fromBody: `goodRequestApiModel` | `goodApiModel` | 200 OK<br/>404 NotFound |
|DELETE|api/works/{id}|Удаляет товар с идентификатором id | fromRoute: id | | 200 OK<br/>404 NotFound |


```javascript
// goodItem
{
  id: 1,
  name: "Товар 1",
  description: "описание товара 1",
  price: 10000
}
```
```javascript

// goodRequestApiModel
{
  name: "Товар 1",
  description: "описание товара 1",
  price: 10000
}
```
