using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Text.Json;

namespace HakimLivs.Data
{    
    public class HakimData
    {
        public List<DataObject> data { get; set; }
    }

    public class DataObject
    {
        public Attribute attributes { get; set; }
    }

    public class Attribute
    {
        public string title { get; set; }
        public Body body { get; set; }
        public List<Category> computed_categories { get; set; }
        public Brand? computed_brand { get; set; }
        public List<Variation> computed_variations { get; set; }

    }

    public class Body
    {
        public string value { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
    }

    public class Brand
    {
        public string? name { get; set; }
    }

    public class Variation
    {
        public List<Image> computed_images { get; set; }
        public Price normal_price { get; set; }
        public RPrice resolved_price { get; set; }
        public List<DiscountPrice> prices { get; set; }
        public string country_of_origin_full { get; set; }
        public string best_before { get; set; }
        public int stock { get; set; }
        public Measurement measurement { get; set; }
    }

    public class Image
    {
        public Style styles { get; set; }
    }

    public class Style
    {
        public string catalog { get; set; }
    }

    public class Price
    {
        public string number { get; set; }
    }

    public class RPrice
    {
        public string number { get; set; }
    }

    public class DiscountPrice
    {
        public DPrice price { get; set; }
    }

    public class Measurement
    {
        public string unit { get; set; }
        public string number { get; set; }
    }

    public class DPrice
    {
        public string number { get; set; }
    }


    public static class DbInitializer
    {

        /// <summary>
        /// Initializes the database with test data.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="userManager">The user manager.</param>
        public static async Task InitializeAsync(ApplicationDbContext database, UserManager<IdentityUser> userManager)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            if (database.Products.Any())
            {
                return;
            }

            await GetProductsAsync(database);

            if (!database.Users.Any())
            {
                // Hakim
                var hakimEmail = "hakim@example.com";
                var hakimUser = new IdentityUser(hakimEmail);
                hakimUser.Email = hakimEmail;
                hakimUser.EmailConfirmed = true;
                await userManager.CreateAsync(hakimUser, "Test123!");

                var hakim = new User
                {
                    IdentityUser = hakimUser,
                    FirstName = "Hakim",
                    LastName = "Hakimsson",
                    Address = new Address { Street = "Hittepågatan 1", ZipCode = "12345", City = "Bollnäs" },
                    IsAdmin = true,
                };
                database.Users.Add(hakim);

                // User 1
                var testEmail = "user@example.com";
                var testUser = new IdentityUser(testEmail);
                testUser.Email = testEmail;
                testUser.EmailConfirmed = true;
                await userManager.CreateAsync(testUser, "Test123!");

                var user = new User
                {
                    IdentityUser = testUser,
                    FirstName = "Test",
                    LastName = "Testsson",
                    Address = new Address { Street = "Gatan 1", ZipCode = "12321", City = "Stockholm" }
                };
                database.Users.Add(user);

                // User 2
                var testEmail2 = "user2@example.com";
                var testUser2 = new IdentityUser(testEmail2);
                testUser2.Email = testEmail2;
                testUser2.EmailConfirmed = true;
                await userManager.CreateAsync(testUser2, "Test123!");

                var user2 = new User
                {
                    IdentityUser = testUser2,
                    FirstName = "Håkan",
                    LastName = "Hellström",
                    Address = new Address { Street = "Feskekörka", ZipCode = "66655", City = "Göteborg" }
                };
                database.Users.Add(user2);

                // User 3
                var testEmail3 = "user3@example.com";
                var testUser3 = new IdentityUser(testEmail3);
                testUser3.Email = testEmail3;
                testUser3.EmailConfirmed = true;
                await userManager.CreateAsync(testUser3, "Test123!");

                var user3 = new User
                {
                    IdentityUser = testUser3,
                    FirstName = "Laban",
                    LastName = "Spöksson",
                    Address = new Address { Street = "Gatstigen 5", ZipCode = "13467", City = "Luleå" }
                };
                database.Users.Add(user3);

                // User 4
                var testEmail4 = "user4@example.com";
                var testUser4 = new IdentityUser(testEmail4);
                testUser4.Email = testEmail4;
                testUser4.EmailConfirmed = true;
                await userManager.CreateAsync(testUser4, "Test123!");

                var user4 = new User
                {
                    IdentityUser = testUser4,
                    FirstName = "Sven",
                    LastName = "Boll",
                    Address = new Address { Street = "Gränd 7", ZipCode = "99887", City = "Malmö" }
                };
                database.Users.Add(user4);

                // User 5
                var testEmail5 = "user5@example.com";
                var testUser5 = new IdentityUser(testEmail5);
                testUser5.Email = testEmail5;
                testUser5.EmailConfirmed = true;
                await userManager.CreateAsync(testUser5, "Test123!");

                var user5 = new User
                {
                    IdentityUser = testUser5,
                    FirstName = "Mona",
                    LastName = "Lisa",
                    Address = new Address { Street = "Rue de Rivoli", ZipCode = "75001", City = "Paris" }
                };
                database.Users.Add(user5);

                await database.SaveChangesAsync();
            }
        }
        // Code from https://alialhaddad.medium.com/how-to-fetch-data-in-c-net-core-ea1ab720e3f9        
        /// <summary>
        /// Gets products from API and saves them to database.
        /// </summary>
        /// <returns></returns>
        static async Task GetProductsAsync(ApplicationDbContext database)
        {
            string baseURL = $"https://api.motatos.se/jsonapi/index/products?page[limit]=50&sort=-sticky,-created&filter[exclude_from_lists][condition][path]=exclude_from_lists&filter[exclude_from_lists][condition][operator]=%3C%3E&filter[exclude_from_lists][condition][value]=1&filter[out_of_stock][condition][path]=out_of_stock&filter[out_of_stock][condition][operator]=%3C%3E&filter[out_of_stock][condition][value]=1";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                var dataObj = JsonSerializer.Deserialize<HakimData>(data);
                                //var listObjects = dataObj.Name;
                                Console.WriteLine($"Name: {dataObj.data[0].attributes.title}");

                                foreach (var item in dataObj.data)
                                {
                                    string name = item.attributes.title;

                                    string d = item.attributes.body.value;
                                    string description = Utils.StripHTML(d);

                                    string category = item.attributes.computed_categories[0].name;

                                    string brand = "";
                                    if (item.attributes.computed_brand != null)
                                    {
                                        brand = item.attributes.computed_brand.name;
                                    }
                                    string image = item.attributes.computed_variations[0].computed_images[0].styles.catalog;

                                    double price = 0;
                                    if (item.attributes.computed_variations[0].normal_price != null)
                                    {
                                        string p = item.attributes.computed_variations[0].normal_price.number ?? "";
                                        if (p != null && p != "")
                                        {
                                            price = Convert.ToDouble(p);
                                        }
                                    }
                                    else
                                    {
                                        price = Convert.ToDouble(item.attributes.computed_variations[0].resolved_price.number);
                                    }

                                    double? discountPrice = null;
                                    string discount = item.attributes.computed_variations[0].prices[0].price.number;
                                    if (discount != null && discount != "")
                                    {
                                        discountPrice = Convert.ToDouble(discount);
                                    }

                                    string origin = item.attributes.computed_variations[0].country_of_origin_full;

                                    DateTime? expirationDate = null;
                                    if (item.attributes.computed_variations[0].best_before != null)
                                    {
                                        expirationDate = DateTime.Parse(item.attributes.computed_variations[0].best_before);
                                    }

                                    int stock = item.attributes.computed_variations[0].stock;
                                    string unitType = item.attributes.computed_variations[0].measurement.unit;
                                    double unitValue = double.Parse(item.attributes.computed_variations[0].measurement.number);
                                    double comparisonPrice = 1 / unitValue * price;

                                    var product = new Product
                                    {
                                        Name = name,
                                        Description = description,
                                        Category = category,
                                        Brand = brand,
                                        Image = image,
                                        Price = price,
                                        DiscountPrice = discountPrice,
                                        ComparisonPrice = comparisonPrice,
                                        Origin = origin,
                                        ExpirationDate = expirationDate,
                                        Stock = stock,
                                        UnitType = unitType,
                                        UnitValue = unitValue
                                    };
                                    
                                    database.Products.Add(product);                              
                                }
                                await database.SaveChangesAsync();
                            }
                            else
                            {
                                //If data is null log it into console.
                                Console.WriteLine("Data is null!");
                            }
                        }
                    }
                }
                //Catch any exceptions and log it into the console.
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
