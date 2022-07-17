export interface LoginResponse {
  success: boolean;
  token: string;
  refreshToken: string;
  errors: string[];
}
