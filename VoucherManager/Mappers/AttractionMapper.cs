using VoucherManager.DTOs;
using VoucherManager.Models;

namespace VoucherManager.Mappers;

public class AttractionMapper
{
    public static AttractionDTO toAttractionDto(Attraction attraction)
    {
        return new AttractionDTO
        {
            Content = attraction.Content
        };
    }
}
