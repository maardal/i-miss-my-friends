<script lang="ts">
	import { setHangoutState } from '$lib/stores/HangoutStore.svelte';

	let { children } = $props();

	setHangoutState();
</script>

{@render children()}
