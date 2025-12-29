import { ErrorHandler } from "@angular/core";
import { LoggerService } from "../logging/logger-service";

export class GlobalErrorHandler implements ErrorHandler {
  constructor(private logger: LoggerService) {}

  handleError(error: unknown): void {
    this.logger.error('Unhandled UI Error', error);
    console.error(error);
  }
}
