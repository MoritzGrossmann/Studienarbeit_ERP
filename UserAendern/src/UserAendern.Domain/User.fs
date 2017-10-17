namespace UserAendern.Domain

type User = {
    FirstName : string
    LastName : string
    FullName : string
    UserName : string
}

type Address = {
    Street : string
    Number : string
    postcode : string
    city : string
}

type UserDetails = {
        BirthName : string
    }

type UserCreate = {
    User: User
    Address : Address
}

type SaveResponse = {
    Erfolg : bool
    Message : string
}

type ISaveUser = 
        abstract CreateUser : user : User -> SaveResponse
        abstract DeleteUser : user : User -> SaveResponse
        abstract ChangeUser : user : User -> address : Address -> SaveResponse
        abstract LockUser   : user : string -> SaveResponse
        abstract UnlockUser : user : string -> SaveResponse
    

type ILoadUser = 
    abstract GetUsers : User seq
    abstract GetUserAddress : username : string -> Address
    