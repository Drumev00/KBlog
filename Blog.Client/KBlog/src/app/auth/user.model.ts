export class User {
  constructor(
    public id: string,
    public username: string,
    public createdOn: Date,
    public email: string,
    public profilePic: string
  ) {}
}
