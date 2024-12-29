# Money API

Money API 是一個 ASP.NET Core 的 Web API，提供貨幣相關的操作，例如查詢貨幣列表、單一貨幣資訊以及貨幣匯率等功能。實現了 Swagger UI

---

## 功能

- **貨幣管理**
  - 查詢所有貨幣
  - 查詢單一貨幣
  - 新增貨幣
  - 刪除貨幣
  - 更新貨幣
- **匯率查詢**
  - 從外部 API 獲取匯率資訊

---

## 架構

- **後端框架**: ASP.NET Core 8
- **資料庫**: Entity Framework Core (InMemory)
- **測試框架**: NUnit、Moq
- **API 文件**: Swagger UI