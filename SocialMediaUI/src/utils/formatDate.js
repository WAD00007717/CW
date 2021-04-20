import { formatDistance } from "date-fns";

export const formatDate = (date) =>
  formatDistance(new Date(date), new Date(), { addSuffix: true });
