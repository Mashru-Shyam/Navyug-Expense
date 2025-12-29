import { HttpInterceptorFn } from '@angular/common/http';

export const correlationInterceptor: HttpInterceptorFn = (req, next) => {
  const header = 'X-Correlation-Id';

  let correlationId = req.headers.get(header);

  if (!correlationId) {
    correlationId = crypto.randomUUID();
  }

  const request = req.clone({
    setHeaders: {
      [header]: correlationId
    }
  });

  return next(request);
};
