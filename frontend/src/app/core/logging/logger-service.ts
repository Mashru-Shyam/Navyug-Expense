import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";

export type LogLevel = 'INFO' | 'WARN' | 'ERROR';
@Injectable({
  providedIn: 'root',
})

export class LoggerService {
  private correlationId = crypto.randomUUID();

  constructor(private http: HttpClient) {}

  log(level: LogLevel, message: string, data: any) {
    const logEntry = {
      timestamp: new Date().toISOString(),
      level,
      message,
      data,
      correlationId: this.correlationId,
    };

    if (!environment.production) {
      console[level === 'ERROR' ? 'error' : 'log'](logEntry);
    } else {
      this.sendToServer(logEntry);
    }
  }

  info(message: string, data: any) {
    this.log('INFO', message, data);
  }

  warn(message: string, data: any) {
    this.log('WARN', message, data);
  }

  error(message: string, data: any) {
    this.log('ERROR', message, data);
  }

  private sendToServer(log: any) {
    this.http.post('/log/frontend',JSON.stringify(log))
  }
}
