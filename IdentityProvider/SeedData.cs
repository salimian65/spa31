// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityProvider.Data;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityProvider
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var customer = roleMgr.FindByNameAsync("customer").Result;
                    if (customer == null)
                    {
                        customer = new IdentityRole
                        {
                            Name = "customer"
                        };
                        _ = roleMgr.CreateAsync(customer).Result;
                    }

                    //------------------------------------------------------------------------
                    var server = roleMgr.FindByNameAsync("server").Result;
                    if (server == null)
                    {
                        server = new IdentityRole
                        {
                            Name = "server"
                        };
                        _ = roleMgr.CreateAsync(server).Result;
                    }

                    //------------------------------------------------------------------------
                    var admin = roleMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole
                        {
                            Name = "admin"
                        };
                        _ = roleMgr.CreateAsync(admin).Result;
                    }

                    ///////////////////////////////////////////////////////////////////
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    ////////////////////////////////////////////////////////////////////////
                    var mohammad = userMgr.FindByNameAsync("mohammad").Result;
                    if (mohammad == null)
                    {
                        mohammad = new ApplicationUser
                        {
                            UserName = "mohammad",
                            Email = "mohammad@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(mohammad, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(mohammad, customer.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(mohammad, customer.Name).Result;
                        }

                        result = userMgr.AddClaimsAsync(mohammad, new Claim[]{
                            new Claim(JwtClaimTypes.Role, customer.Name),
                            new Claim(JwtClaimTypes.Name, "mohammad mohammady"),
                            new Claim(JwtClaimTypes.GivenName, "mohammad"),
                            new Claim(JwtClaimTypes.FamilyName, "mohammady"),
                            new Claim(JwtClaimTypes.WebSite, "http://mohammady.com"),
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("mohammad created");
                    }
                    else
                    {
                        Log.Debug("mohammad already exists");
                    }

                    // ------------------------------------------------------------------------
                    var mehrdad = userMgr.FindByNameAsync("mehrdad").Result;
                    if (mehrdad == null)
                    {
                        mehrdad = new ApplicationUser
                        {
                            UserName = "mehrdad",
                            Email = "salimian.mehrdad@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(mehrdad, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(mehrdad, customer.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(mehrdad, customer.Name).Result;
                        }

                        result = userMgr.AddClaimsAsync(mehrdad, new Claim[]{
                            new Claim(JwtClaimTypes.Role, customer.Name),
                            new Claim(JwtClaimTypes.Name, "Mehrdad Salimian"),
                            new Claim(JwtClaimTypes.GivenName, "Mehrdad"),
                            new Claim(JwtClaimTypes.FamilyName, "Salimian"),
                            new Claim(JwtClaimTypes.WebSite, "http://Mehrdad.com"),
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        Log.Debug("mehrdad created");
                    }
                    else
                    {
                        Log.Debug("mehrdad already exists");
                    }

                    //------------------------------------------------------------------------
                    var elham = userMgr.FindByNameAsync("elham").Result;
                    if (elham == null)
                    {
                        elham = new ApplicationUser
                        {
                            UserName = "elham",
                            Email = "elham@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(elham, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(elham, server.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(elham, server.Name).Result;
                        }

                        result = userMgr.AddClaimsAsync(elham, new Claim[]{
                            new Claim(JwtClaimTypes.Role, server.Name),
                            new Claim(JwtClaimTypes.Name, "Elham Shamouli"),
                            new Claim(JwtClaimTypes.GivenName, "Elham"),
                            new Claim(JwtClaimTypes.FamilyName, "Shamouli"),
                            new Claim(JwtClaimTypes.WebSite, "http://Elham.com"),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }



                        Log.Debug("elham created");
                    }
                    else
                    {
                        Log.Debug("elham already exists");
                    }

                    //------------------------------------------------------------------------
                    var majid = userMgr.FindByNameAsync("majid").Result;
                    if (majid == null)
                    {
                        majid = new ApplicationUser
                        {
                            UserName = "majid",
                            Email = "majid@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(majid, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(majid, server.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(majid, server.Name).Result;
                        }

                        result = userMgr.AddClaimsAsync(majid, new Claim[]{
                            new Claim(JwtClaimTypes.Role, server.Name),
                            new Claim(JwtClaimTypes.Name, "Majid Majidi"),
                            new Claim(JwtClaimTypes.GivenName, "Majid"),
                            new Claim(JwtClaimTypes.FamilyName, "Majidi"),
                            new Claim(JwtClaimTypes.WebSite, "http://Majidi.com"),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }



                        Log.Debug("Majidi created");
                    }
                    else
                    {
                        Log.Debug("Majidi already exists");
                    }

                    //------------------------------------------------------------------------
                    var behcet = userMgr.FindByNameAsync("behcet").Result;
                    if (behcet == null)
                    {
                        behcet = new ApplicationUser
                        {
                            UserName = "behcet",
                            Email = "behcet@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(behcet, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(behcet, admin.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(behcet, admin.Name).Result;
                        }

                        result = userMgr.AddClaimsAsync(behcet, new Claim[]{
                            new Claim(JwtClaimTypes.Role, admin.Name),
                            new Claim(JwtClaimTypes.Name, "Behcet Ghahreman"),
                            new Claim(JwtClaimTypes.GivenName, "Behcet"),
                            new Claim(JwtClaimTypes.FamilyName, "Ghahreman"),
                            new Claim(JwtClaimTypes.WebSite, "http://Ghahreman.com"),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }



                        Log.Debug("behcet created");
                    }
                    else
                    {
                        Log.Debug("behcet already exists");
                    }
                }
            }

            //using (var serviceProvider = services.BuildServiceProvider())
            //{
            //    using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //    {
            //        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            //        context.Database.Migrate();

            //        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //        var alice = userMgr.FindByNameAsync("alice").Result;
            //        if (alice == null)
            //        {
            //            alice = new ApplicationUser
            //            {
            //                UserName = "alice"
            //            };
            //            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }

            //            result = userMgr.AddClaimsAsync(alice, new Claim[]{
            //            new Claim(JwtClaimTypes.Name, "Alice Smith"),
            //            new Claim(JwtClaimTypes.GivenName, "Alice"),
            //            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
            //            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            //            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
            //        }).Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }
            //            Log.Debug("alice created");
            //        }
            //        else
            //        {
            //            Log.Debug("alice already exists");
            //        }

            //        var bob = userMgr.FindByNameAsync("bob").Result;
            //        if (bob == null)
            //        {
            //            bob = new ApplicationUser
            //            {
            //                UserName = "bob"
            //            };
            //            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }

            //            result = userMgr.AddClaimsAsync(bob, new Claim[]{
            //            new Claim(JwtClaimTypes.Name, "Bob Smith"),
            //            new Claim(JwtClaimTypes.GivenName, "Bob"),
            //            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
            //            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            //            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
            //            new Claim("location", "somewhere")
            //        }).Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }
            //            Log.Debug("bob created");
            //        }
            //        else
            //        {
            //            Log.Debug("bob already exists");
            //        }
            //    }
            //}
        }
    }
}