// Wms.Application/DTOs/ItemDto.cs

namespace Wms.Application.DTOs;

public record ItemDto(
    int Id,
    string Sku,
    string Name,
    string Description,
    string UnitOfMeasure,
    bool IsActive,
    bool RequiresLot,
    bool RequiresSerial,
    int ShelfLifeDays,
    decimal? Price,
    List<string> Barcodes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateItemDto(
    string Sku,
    string Name,
    string Description,
    string UnitOfMeasure,
    bool RequiresLot = false,
    bool RequiresSerial = false,
    int ShelfLifeDays = 0,
    decimal? Price = null,
    List<string> Barcodes = null!
)
{
    public List<string> Barcodes { get; init; } = Barcodes ?? new List<string>();
}

public record UpdateItemDto(
    string Name,
    string Description,
    int ShelfLifeDays = 0,
    decimal? Price = null
);