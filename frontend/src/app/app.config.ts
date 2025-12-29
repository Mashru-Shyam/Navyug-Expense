import { ApplicationConfig, ErrorHandler, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { correlationInterceptor } from './core/interceptors/correlation-interceptor';
import { baseUrlInterceptor } from './core/interceptors/base-url-interceptor';
import { GlobalErrorHandler } from './core/error/GlobalErrorHandler';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      correlationInterceptor,
      baseUrlInterceptor
    ])),
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
  ]
};
