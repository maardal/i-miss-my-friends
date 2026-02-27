<script lang="ts">
	import Footer from '$lib/components/Footer.svelte';
	import Header from '$lib/components/Header.svelte';
	import { setHangoutState } from '$lib/stores/HangoutStore.svelte';
	import { setLovedOneState } from '$lib/stores/LovedOneStore.svelte';

	let { children } = $props();

	setHangoutState();
	setLovedOneState();
</script>

<main class="layout">
	<Header></Header>
	{@render children()}
	<Footer></Footer>
</main>
