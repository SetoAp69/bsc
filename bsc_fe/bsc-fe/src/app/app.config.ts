import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './interceptors/auth.interceptor';
import { provideLottieOptions } from 'ngx-lottie';
import player from 'lottie-web';


export const appConfig: ApplicationConfig = {
  providers: [
    provideLottieOptions({player:()=>player}),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([authInterceptor])
    )
  ]
};
