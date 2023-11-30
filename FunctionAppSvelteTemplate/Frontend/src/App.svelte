<script lang="ts">
  import svelteLogo from './assets/svelte.svg'
  import viteLogo from '/vite.svg'
  import Counter from './lib/Widget.svelte'
  import { Appsettings } from '../Appsettings'
  import Widget from './lib/Widget.svelte';
  import { widgets, WidgetData, addWidget, removeWidget } from './services/WidgetService';

  if(Appsettings.Built)
  {
     //Logic for when built by the function app
  }

  let widgetsCurrent;
  let widgetsUnsubscribe = widgets.subscribe(x => widgetsCurrent = x);

  function onContext(e:MouseEvent){
    e.preventDefault();
    addWidget(new WidgetData(
      e.clientX,
      e.clientY
    ));
  }
</script>

<main on:contextmenu={onContext}>
  {#each widgetsCurrent as widget (widget.id)}
    <Widget data={widget} title={"Exremely Long Named Test Window"}>
      <p>({widget.x}, {widget.y})</p>
      <p>initialized: {widget.initialized}</p>
    </Widget>
  {/each}
</main>

<style>
  main{
    position: absolute;
    left:0;
    right: 0;
    top:0;
    bottom: 0;
  }
</style>
