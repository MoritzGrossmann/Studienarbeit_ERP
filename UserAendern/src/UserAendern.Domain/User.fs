namespace UserAendern.Domain

type User = {
    VorName : string
    NachName : string
    FullName : string
    UserName : string
}

type UserDetails = {
        BirthName : string
    }

type ISaveUser = 
        abstract CreateUser : user : User -> bool
        abstract DeleteUser : user : User -> bool
        abstract ChangeUser : user : User -> bool
    

type ILoadUser = 
    abstract GetUsers : User seq
    