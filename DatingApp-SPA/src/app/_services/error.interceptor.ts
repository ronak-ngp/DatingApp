import { Injectable, Provider } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable({
    providedIn: 'root'
  })
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
         if (error instanceof HttpErrorResponse) {
            if (error.status === 401 ) {
                return throwError(error.statusText);
            }
            const serverError = error.error;
            let modalStateErrors = '';
            if (serverError && typeof serverError === 'object') {
            for (const key in serverError) {
                if (serverError[key]) {
                    modalStateErrors += serverError[key] + '\n';
                }
            }
           }
            return throwError(modalStateErrors || serverError || 'ServerError');
         }
        })
      );
    }
}

export const ErrorInterceptorProvider: Provider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
