﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowTutorials.ConsoleClient.Common.Data;

internal static class StaticData
{
    public static readonly string JsonRegistrationText = """
                                                    {
                                                      "RegistrationDate": "2025-04-23T14:30:00Z",
                                                      "FirstName": "Conan",
                                                      "Surname": "Druid",
                                                      "Age": 28,
                                                      "EmailAddress": "conan.druid@fake-stonehenge.com",
                                                      "AddressLine": "Stonehenge",
                                                      "Town": "Amesbury",
                                                      "City": "Salisbury",
                                                      "County": "Wiltshire",
                                                      "PostCode": "SP4 7DE"
                                                    }
                                                """;


    public static readonly string JsonBadRegistrationText = """
                                                    {
                                                      "RegistrationDate": "2025-04-23T14:30:00Z",
    
                                                      "Surname": "Druid",
                                                      "Age": 28,
                                                      "EmailAddress": "conan.druid@fake-stonehenge.com",
                                                      "AddressLine": "Stonehenge",
                                                      "Town": "Amesbury",
                                                      "City": "Salisbury",
                                                      "County": "Wiltshire",
                                                      "PostCode": "SP4 7DE"
                                                    }
                                                """;

}
