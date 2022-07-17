
export class CreateTimesheetRequest {
  public userId: string;
  public description: string;
  public date: Date;
  public timeIn: string;
  public timeOut: string;
  public isAbsent: boolean = false;
  public isHoliday: boolean = false;

}
