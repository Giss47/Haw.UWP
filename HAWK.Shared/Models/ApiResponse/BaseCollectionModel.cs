using System;
using System.Collections.Generic;
using System.Text;

namespace HAWK.Shared.Models.ApiResponse
{
    public class BaseCollectionModel<T>
    {
        public T[] Items { get; set; }
    }
}