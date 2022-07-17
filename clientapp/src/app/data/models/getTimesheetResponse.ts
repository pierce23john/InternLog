export class GetTimesheetResponse {
  id: string;
  description: string;
  date: Date;
  timeIn: Date;
  timeOut: Date;
  userId: string;
  hoursRendered: number;
}
