import { writable, type Writable } from 'svelte/store';
import { generateGUID } from '../utilities/GuidGenerator';

export interface IWidget {
  id:string
  initialized:boolean;
  x:number;
  y:number;
  update:(property:string, val:any) => void;
  onDestroy?:()=>void;
}

export class WidgetData implements IWidget{
    id:string
    initialized:boolean;
    x:number;
    y:number;
    update:(property:string, val:any) => void;
    onDestroy?:()=>void

    constructor(x:number, y:number, onDestroy?:()=>void
    ){
        this.id = generateGUID();
        this.initialized = false;
        this.x = x;
        this.y = y;
        this.onDestroy = onDestroy;
    }
}

export const widgets: Writable<IWidget[]> = writable([]);

export function addWidget(widget: IWidget): void {
    let originalOnDestroy = widget.onDestroy ?? (() => {});
    let guid = widget.id;

    widget.onDestroy = () => {
        originalOnDestroy();
        removeWidget(guid);
    }

    widget.update = (property:string, val:number) => {
        widget[property] = val;
        widgets.update(widgetsArray => [...widgetsArray]);
    }

    widgets.update(widgetsArray => [...widgetsArray, widget]);
}

export function removeWidget(widgetId: string): void {
    widgets.update(widgetsArray => widgetsArray.filter(w => w.id !== widgetId));
}