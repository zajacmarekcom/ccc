using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Features.Agent.Home
{
    public class PageViewModel
    {
        public PageViewModel(int currentPage, int pageCount)
        {
            CurrentPage = currentPage;
            PageCount = pageCount;

            StartPage = CalculateStartPage();
            EndPage = CalculateEndPage();

            IsLastPage = CurrentPage == PageCount;
        }

        public int CurrentPage { get; }
        public int PageCount { get; }
        public int StartPage { get; }
        public int EndPage { get; }
        public bool IsLastPage { get; }

        private int CalculateStartPage()
        {
            if (CurrentPage > 4)
            {
                return CurrentPage - 3;
            }
            return 1;
        }

        private int CalculateEndPage()
        {
            if (CurrentPage > PageCount - 3)
            {
                return PageCount;
            }
            return CurrentPage - 3;
        }
    }
}