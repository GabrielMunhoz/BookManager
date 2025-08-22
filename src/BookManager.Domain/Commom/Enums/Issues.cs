namespace BookManager.Domain.Commom.Enums;
public enum Issues
{
    none,
    #region Controllers

    #region Information
    /// <summary>
    /// LoanController - CreateAsync - Information
    /// </summary>
    i001,
    /// <summary>
    /// LoanController - GetAllPagedAsync - Information
    /// </summary>
    i002,
    /// <summary>
    /// LoanController - RequestReturnBookAsync - Information
    /// </summary>
    i003,
    /// <summary>
    /// LoanController - ReturnBookAsync - Information
    /// </summary>
    i004,
    /// <summary>
    /// BookController - CreateAsync - Information
    /// </summary>
    i005,
    /// <summary>
    /// BookController - GetAllAsync - Information
    /// </summary>
    i006,
    /// <summary>
    /// BookController - GetBookByIdAsync - Information
    /// </summary>
    i007,
    /// <summary>
    /// BookController - UpdateAsync - Information
    /// </summary>
    i008,
    /// <summary>
    /// BookController - DeleteByIdAsync - Information
    /// </summary>
    i009,
    /// <summary>
    /// UsersController - CreateAsync - Information
    /// </summary>
    i010,
    /// <summary>
    /// UsersController - GetAllAsync - Information
    /// </summary>
    i011,
    /// <summary>
    /// UsersController - GetUserByIdAsync - Information
    /// </summary>
    i012,
    /// <summary>
    /// UsersController - UpdateAsync - Information
    /// </summary>
    i013,
    /// <summary>
    /// UsersController - DeleteByIdAsync - Information
    /// </summary>
    i014,

    #endregion

    #region Errors
    /// <summary>
    /// 400 BadRequest
    /// </summary>
    e400,
    /// <summary>
    /// 500 InternalServerError
    /// </summary>
    e500,
    #endregion

    #endregion

    #region Services
    #region Information
    /// <summary>
    /// LoanService - CreateAsync - Information
    /// </summary>
    i1000,
    /// <summary>
    /// LoanService - GetAllAsync - Information
    /// </summary>
    i1001,
    /// <summary>
    /// LoanService - RequestReturnBookAsync - Information
    /// </summary>
    i1002,
    /// <summary>
    /// LoanService - ReturnBookAsync - Information
    /// </summary>
    i1003,
    /// <summary>
    /// BookService - CreateAsync - Information
    /// </summary>
    i1004,
    /// <summary>
    /// BookService - GetAllAsync - Information
    /// </summary>
    i1005,
    /// <summary>
    /// BookService - GetByIdAsync - Information
    /// </summary>
    i1006,
    /// <summary>
    /// BookService - UpdateAsync - Information
    /// </summary>
    i1007,
    /// <summary>
    /// BookService - DeleteByIdAsync - Information
    /// </summary>
    i1008,
    /// <summary>
    /// UsersService - CreateAsync - Information
    /// </summary>
    i1009,
    /// <summary>
    /// UsersService - GetAllAsync - Information
    /// </summary>
    i1010,
    /// <summary>
    /// UsersService - UpdateAsync - Information
    /// </summary>
    i1011,
    /// <summary>
    /// UsersService - DeleteByIdAsync - Information
    /// </summary>
    i1012,
    /// <summary>
    /// BookService - GetQueryAsync - Information
    /// </summary>
    i1013,

    #endregion

    #region Errors
    /// <summary>
    /// LoanService - CreateAsync - ValidateError
    /// </summary>
    e1000,
    /// <summary>
    /// LoanService - CreateAsync - ValidateAndReturnExistingBooks
    /// </summary>
    e1001,
    /// <summary>
    /// LoanService - CreateAsync - ValidateAndReturnExistingUser
    /// </summary>
    e1002,
    /// <summary>
    /// LoanService - RequestReturnBookAsync - LoanNotFound
    /// </summary>
    e1003,
    /// <summary>
    /// LoanService - ReturnBookAsync - LoanNotFound
    /// </summary>
    e1004,
    /// <summary>
    /// LoanService - ReturnBookAsync - Update loan failed
    /// </summary>
    e1005,
    /// <summary>
    /// BookService - CreateAsync - Create book failed
    /// </summary>
    e1006,
    /// <summary>
    /// BookService - GetByIdAsync - Book not found
    /// </summary>
    e1007,
    /// <summary>
    /// BookService - UpdateAsync - Update book failed
    /// </summary>
    e1008,
    /// <summary>
    /// BookService - DeleteByIdAsync - Delete book failed
    /// </summary>
    e1009,
    /// <summary>
    /// UsersService - CreateAsync - Create user failed
    /// </summary>
    e1010,
    /// <summary>
    /// UsersService - GetByIdAsync - User not found
    /// </summary>
    e1011,
    /// <summary>
    /// UsersService - UpdateAsync - Update user failed
    /// </summary>
    e1012,
    /// <summary>
    /// UsersService - DeleteByIdAsync - Delete user failed
    /// </summary>
    e1013,
    /// <summary>
    /// LoanService - CreateAsync - Validate book in stock
    /// </summary>
    e1014,
    /// <summary>
    /// LoanService - CreateAsync - Error on creating 
    /// </summary>
    e1015,
    /// <summary>
    /// LoanService - ReturnBookAsync - Execute payment failed
    /// </summary>
    e1016,
    /// <summary>
    /// LoanService - ReturnBookAsync - Exception
    /// </summary>
    e1017,
    #endregion
    #endregion
}
