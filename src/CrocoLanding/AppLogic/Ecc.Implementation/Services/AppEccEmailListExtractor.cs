﻿using Croco.Core.Abstractions.Application;
using Croco.Core.Abstractions.Models;
using Ecc.Logic.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecc.Implementation.Services
{
    public class AppEccEmailListExtractor : IEccFileEmailsExtractor
    {
        public AppEccEmailListExtractor(ICrocoApplication app)
        {
            App = app;
        }

        ICrocoApplication App { get; }

        string MapPath(string filePath) => App.MapPath(filePath);

        public async Task<BaseApiResponse<List<string>>> ExtractEmailsListFromFile(string filePath)
        {
            filePath = MapPath(filePath);

            if (!File.Exists(filePath))
            {
                return new BaseApiResponse<List<string>>(false, "Файл не существует по указанному пути");
            }

            var res = (await File.ReadAllTextAsync(filePath)).Split(',').ToList();
    
            return new BaseApiResponse<List<string>>(true, "Ok", res);
        }

        public bool FileExists(string filePath)
        {
            filePath = MapPath(filePath);

            return File.Exists(filePath);
        }
    }
}