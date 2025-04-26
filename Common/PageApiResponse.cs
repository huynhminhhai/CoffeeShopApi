using CoffeeShopApi.Common;

public class PageApiResponse<T> : ApiResponse<T>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalItems { get; set; }
        public int? TotalPages { get; set; }

        public PageApiResponse() { }

        public PageApiResponse(
            bool success,
            T? data,
            string message = "",
            int statusCode = 200,
            int? pageIndex = null,
            int? pageSize = null,
            int? totalItems = null,
            int? totalPages = null)
            : base(success, data, message, statusCode)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public static PageApiResponse<T> SuccessPageResponse(
            T data,
            int pageIndex,
            int pageSize,
            int totalItems,
            string message = "",
            int statusCode = 200)
        {
            return new PageApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }