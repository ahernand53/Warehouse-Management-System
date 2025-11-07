// Wms.Application/DTOs/LotDto.cs

namespace Wms.Application.DTOs;

public record LotDto(
    int Id,
    string Number,
    int ItemId,
    string ItemSku,
    string? ItemName,
    DateTime? ExpiryDate,
    DateTime? ManufacturedDate,
    bool IsActive
);

