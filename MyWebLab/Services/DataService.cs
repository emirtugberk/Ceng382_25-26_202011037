// Inside Services/DataService.cs
 using Ceng382_25_26_202011037.Models; // Assuming your Product model is in this namespace
 using System.Text.Json;

 namespace Ceng382_25_26_202011037.Services
 {
    public class DataService
    {
        private readonly string _jsonFilePath;
        private List<Product>? _products; // Cache the loaded products

        // Constructor injection for IWebHostEnvironment to get path information
        public DataService(IWebHostEnvironment webHostEnvironment)
        {
            // Construct the path relative to the web root (ContentRootPath is usually the project root)
            // Assumes your JSON file is named "products.json" and located in a "Data" folder
            _jsonFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "products.json");
        }

        public List<Product> GetProducts()
        {
            // Simple caching: Load only once if _products is null
            if (_products == null)
            {
                try
                {
                    // Check if the file exists before attempting to read
                    if (!File.Exists(_jsonFilePath))
                    {
                        Console.WriteLine($"Error: Product data file not found at {_jsonFilePath}");
                        _products = new List<Product>(); // Return empty list if file missing
                        return _products;
                    }

                    var json = File.ReadAllText(_jsonFilePath);

                    // Use System.Text.Json - ensure PropertyNameCaseInsensitive = true
                    // if your JSON property names (e.g., "id", "name") don't exactly match
                    // your C# property names (e.g., "Id", "Name").
                     _products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
                     {
                         PropertyNameCaseInsensitive = true // Makes matching case-insensitive
                     });
                    // Ensure products list is not null even if deserialization yields null (unlikely for valid list JSON)
                     _products ??= new List<Product>();

                }
                catch (JsonException jsonEx)
                {
                     // Handle JSON specific errors
                     Console.WriteLine($"Error deserializing product data from {_jsonFilePath}: {jsonEx.Message}");
                     _products = new List<Product>(); // Return empty list on error
                }
                catch (IOException ioEx)
                {
                    // Handle file access errors
                    Console.WriteLine($"Error reading product data file {_jsonFilePath}: {ioEx.Message}");
                    _products = new List<Product>(); // Return empty list on error
                }
                catch (Exception ex)
                {
                    // Log the generic error appropriately in a real app (e.g., using ILogger)
                    Console.WriteLine($"An unexpected error occurred loading product data: {ex.Message}");
                    _products = new List<Product>(); // Return empty list on error
                }
            }
             // Return the cached list (or the newly created empty list if an error occurred)
             // Use ?? new List<Product>() as a final safeguard against null, although the logic above should prevent it.
            return _products;
        }
    }
 }

 