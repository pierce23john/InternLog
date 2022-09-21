export const createDateFromTimeString = (time) => {
  const hour = Number(time.split(":")[0]);
  const minute = Number(time.split(":")[1]);


  const currentLocalDate = new Date();
  const date = new Date(Date.UTC(currentLocalDate.getUTCFullYear(), currentLocalDate.getUTCMonth(), currentLocalDate.getUTCDate()));
  date.setHours(hour);
  date.setMinutes(minute);

  return date;
};
