namespace UserAendern.Domain

type User = {
    VorName : string
    NachName : string
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

type ISaveUser = 
        abstract CreateUser : user : User -> bool
        abstract DeleteUser : user : User -> bool
        abstract ChangeUser : user : User -> address : Address -> bool
        abstract LockUser   : user : string -> bool
        abstract UnlockUser : user : string -> bool
    

type ILoadUser = 
    abstract GetUsers : User seq
    abstract GetUserAddress : username : string -> Address
    