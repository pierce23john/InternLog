export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  token: string;
  refreshToken: string;
  errors: string[];
}

export interface RefreshTokenRequest {
  token: string;
  refreshToken: string;
}
