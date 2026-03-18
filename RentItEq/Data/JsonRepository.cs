using System.Text.Json;
using RentItEq.Models;

namespace RentItEq.Data;

internal static class JsonDefaults
{
    public static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
}

public class JsonRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly string _filePath;
    private List<T> _items;

    public JsonRepository(string filePath)
    {
        _filePath = filePath;
        _items = Load();
    }

    public List<T> GetAll() => _items;

    public T? GetById(Guid id) => _items.FirstOrDefault(e => e.Uuid == id);

    public void Add(T entity)
    {
        _items.Add(entity);
        SaveChanges();
    }

    public void SaveChanges()
    {
        var json = JsonSerializer.Serialize(_items, JsonDefaults.Options);
        File.WriteAllText(_filePath, json);
    }

    private List<T> Load()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json, JsonDefaults.Options) ?? [];
    }
}
