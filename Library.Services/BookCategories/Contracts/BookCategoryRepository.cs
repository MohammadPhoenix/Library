﻿using Library.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
        bool IsExistBookCategoryWithThisTitle(string title);
        bool IsThisBookCategoryExist(short bookCategoryId);
    }
}
