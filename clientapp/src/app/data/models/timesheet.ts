export interface GetTimesheetResponse {
  id: string;
  description?: string;
  date: Date;
  timeIn?: Date;
  timeOut?: Date;
  userId: string;
  hoursRendered: number;
}

export interface CreateTimesheetRequest {
  description?: string;
  date: Date;
  timeIn?: string;
  timeOut?: string;
  isAbsent: boolean;
  isHoliday: boolean;
}

export interface CreateTimesheetResponse {
  id: string;
  description?: string;
  date: Date;
  timeIn?: string;
  timeOut?: string;
  userId: string;
  hoursRendered: number;
}
