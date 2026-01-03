import { ErrorHandler, Injectable } from "@angular/core";
import { LoggerService } from "../logging/logger-service";

@Injectable()

export class GlobalErrorHandler implements ErrorHandler {
  constructor(private logger: LoggerService) { }

  handleError(error: unknown): void {
    this.logger.error('Global Exception occurred in .handleError() method', error);
    console.error(error);
  }
}
