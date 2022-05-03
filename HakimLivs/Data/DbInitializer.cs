using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace HakimLivs.Data
{
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

            //if (database.Products.Any())
            //{
            //    return;
            //}

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
                                var dataObj = JObject.Parse(data);
                                var listObjects = dataObj.GetValue("data")?.ToList();

                                foreach (var item in listObjects!)
                                {
                                    string name = item.SelectToken("attributes.title")?.ToString() ?? "";

                                    string d = item.SelectToken("attributes.body.value")?.ToString() ?? "";
                                    string description = HakimLivs.Utils.Utils.StripHTML(d);

                                    string category = item.SelectToken("attributes.computed_categories[0].name")?.ToString() ?? "";

                                    string brand = item.SelectToken("attributes.computed_brand.name")?.ToString() ?? "";
                                    string image = item.SelectToken("attributes.computed_variations[0]" +
                                        ".computed_images[0].styles.catalog")?.ToString() ?? "";

                                    double price = 0;
                                    string p = item.SelectToken("attributes.computed_variations[0]" +
                                        ".normal_price.number")?.ToString() ?? "";
                                    if (p != null && p != "")
                                    {
                                        price = double.Parse(p);
                                    }
                                    else
                                    {
                                        price = double.Parse(item.SelectToken("attributes.computed_variations[0]" +
                                        ".resolved_price.number")?.ToString() ?? "0");
                                    }

                                    double? discountPrice = null;
                                    string discount = item.SelectToken("attributes.computed_variations[0]" +
                                        ".prices[1].price.number")?.ToString() ?? "";
                                    if (discount != null && discount != "")
                                    {
                                        discountPrice = double.Parse(discount);
                                    }

                                    string origin = item.SelectToken("attributes.computed_variations[0]" +
                                        ".country_of_origin_full")?.ToString() ?? "";
                                    var expirationDate = DateTime.Parse(item.SelectToken("attributes.computed_variations[0]" +
                                        ".best_before")?.ToString() ?? DateTime.Now.ToString());
                                    int stock = int.Parse(item.SelectToken("attributes.computed_variations[0]" +
                                        ".stock")?.ToString() ?? "0");
                                    string unitType = item.SelectToken("attributes.computed_variations[0]" +
                                        ".measurement.unit")?.ToString() ?? "";
                                    double unitValue = double.Parse(item.SelectToken("attributes.computed_variations[0]" +
                                        ".measurement.number")?.ToString() ?? "0");
                                    double comparisonPrice = 1/unitValue * price;

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
